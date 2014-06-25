﻿namespace NServiceBus
{
    using Castle.Windsor;

    /// <summary>
    /// Windsor extension to pass an existing Windsor container instance.
    /// </summary>
    public static class WindsorExtensions
    {
        /// <summary>
        /// Use the Windsor passing in a pre-configured container to be used by NServiceBus.
        /// </summary>
        /// <param name="customizations"></param>
        /// <param name="container">The existing container instance.</param>
        public static void ExistingContainer(this ContainerCustomizations customizations, IWindsorContainer container)
        {
            customizations.Settings.Set("ExistingContainer", container);
        }
    }
}