using Donify.API.Models.Entities;

namespace Donify.API.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IDonationRepository Donors { get; set; }
        IDonationRepository Donations { get; set; }
        ICampaignRepository Campaigns { get; set; }
        ICampaignRepository Categories { get; set; }
        IProjectRepository Projects { get; set; }
        IStaffRepository Staffs { get; set; }
        IReceiptRepository Receipts { get; set; }
        IUserRepository Users { get; set; }
        Task<int> SaveAsync();
    }
}

