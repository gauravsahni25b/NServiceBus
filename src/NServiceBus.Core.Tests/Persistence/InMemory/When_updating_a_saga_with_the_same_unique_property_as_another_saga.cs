namespace NServiceBus.SagaPersisters.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus.Saga;
    using NUnit.Framework;

    [TestFixture]
    class When_updating_a_saga_with_the_same_unique_property_as_another_saga
    {
        [Test]
        public async Task It_should_persist_successfully()
        {
            var saga1 = new SagaWithUniquePropertyData {Id = Guid.NewGuid(), UniqueString = "whatever1"};
            var saga2 = new SagaWithUniquePropertyData {Id = Guid.NewGuid(), UniqueString = "whatever"};

            var persister = InMemoryPersisterBuilder.Build(typeof(SagaWithUniqueProperty), typeof(SagaWithTwoUniqueProperties));
            var options = new SagaPersistenceOptions(SagaMetadata.Create(typeof(SagaWithUniqueProperty)));

            await persister.Save(saga1, options);
            await persister.Save(saga2, options);

            Assert.Throws<InvalidOperationException>(async () => 
            {
                var saga = await persister.Get<SagaWithUniquePropertyData>(saga2.Id, options);
                saga.UniqueString = "whatever1";
                await persister.Update(saga, options);
            });
        }

        [Test]
        public async Task It_should_persist_successfully_for_two_unique_properties()
        {
            var saga1 = new SagaWithTwoUniquePropertiesData { Id = Guid.NewGuid(), UniqueString = "whatever1", UniqueInt = 5};
            var saga2 = new SagaWithTwoUniquePropertiesData { Id = Guid.NewGuid(), UniqueString = "whatever", UniqueInt = 37};

            var persister = InMemoryPersisterBuilder.Build(typeof(SagaWithUniqueProperty), typeof(SagaWithTwoUniqueProperties));
            var options = new SagaPersistenceOptions(SagaMetadata.Create(typeof(SagaWithTwoUniqueProperties)));

            await persister.Save(saga1, options);
            await persister.Save(saga2, options);

            Assert.Throws<InvalidOperationException>(async () =>
            {
                var saga = await persister.Get<SagaWithTwoUniquePropertiesData>(saga2.Id, options);
                saga.UniqueInt = 5;
                await persister.Update(saga, options);
            });
        }
    }
}