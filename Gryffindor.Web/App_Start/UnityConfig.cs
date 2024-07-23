using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Gryffindor.Web.Services;

namespace Gryffindor.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ISecurityClientService, SecurityClientService>();
            container.RegisterType<IFeedClientService, FeedClientService>();
            container.RegisterType<IChannelClientService, ChannelClientService>();
            container.RegisterType<IUserProfileClientService, UserProfileClientService>();
            container.RegisterType<IGeneralClientService, GeneralClientService>();
            container.RegisterType<INotificationClientService, NotificationClientService>();
            container.RegisterType<IOrganisationClientService, OrganisationClientService>();
            container.RegisterType<IUserFollowingClientService, UserFollowingClientService>();
            container.RegisterType<IResumePermissionClientService, ResumePermissionClientService>();
            container.RegisterType<ISearchClientService, SearchClientService>();
            container.RegisterType<IUserProfileQualificationClientService, UserProfileQualificationClientService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}