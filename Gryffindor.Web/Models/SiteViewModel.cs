using Gryffindor.Contract.DataModels;
using Gryffindor.Contract.Dto;
using Gryffindor.Contract.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web;

namespace Gryffindor.Web.Models
{
    public class SearchViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "",  MinimumLength = 5)]
        public string Keywords { get; set; }
        public string Location { get; set; }
        [Required]
        public string Criteria { get; set; }
        public int PageSize { get; set; }
        public bool CanApply { get; set; }
        public List<string> KeywordsToExclude { get; set; }
        public IEnumerable<UserProfile> Users { get; set; }
        public FeedsDataModel Feeds { get; set; }
    }

    public class NewFeedViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Text { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }
        public int FeedTypeId { get; set; }
        //[FileExtensions(Extensions = "jpg|jpeg|png", ErrorMessage = "Please upload a valid format")]
        public HttpPostedFileBase ImageFile { get; set; }
        public bool CanPostJobs { get; set; }
    }

    public class FeedsViewModel
    {
        public FeedsDataModel TopFeeds { get; set; }
        public FeedsDataModel SponsoredFeeds { get; set; }
        public FeedsDataModel Feeds { get; set; }
        public IEnumerable<Feed> UserProfileFeeds { get; set; }
    }

    public class InterestsViewModel
    {
        public bool HasInterests { get; set; }
        public IEnumerable<Channel> AllChannels { get; set; }
        public IEnumerable<Channel> UserChannels { get; set; }
    }

    public class BrowseFeedsViewModel
    {
        public IEnumerable<Channel> UserChannels { get; set; }
        public Channel SelectedChannel { get; set; }
        public string PreferredJobArea { get; set; }
        public string HomeTown { get; set; }
    }

    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public UserProfile Profile { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Main Interest")]
        public MainInterest MainInterest { get; set; }
        public bool IsMe { get; set; }
        //[FileExtensions(Extensions = "jpg|jpeg|png", ErrorMessage = "Please upload a valid format")]
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class OrganisationViewModel
    {
        public Organisation Organisation { get; set; }
        [Display(Name = "Organisation Type")]
        public OrganisationType OrganisationType { get; set; }
        public HttpPostedFileBase LogoFile { get; set; }
        public HttpPostedFileBase MainImageFile { get; set; }
    }

    public class QualificationViewModel
    {
        public UserProfile UserProfile { get; set; }
        public UserProfileAchievement UserProfileAchievement { get; set; }
        public UserProfileEducation UserProfileEducation { get; set; }
        public UserProfileSkill UserProfileSkill { get; set; }
        public UserProfileWork UserProfileWork { get; set; }
        [Display(Name = "Qualification Type")]
        public QualificationType QualificationType { get; set; }
    }

    public class ForgotPassowrdTemplateViewModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ResetUrl { get; set; }
        public string SiteUrl { get { return ConfigurationManager.AppSettings["Site"]; } }
    }
}