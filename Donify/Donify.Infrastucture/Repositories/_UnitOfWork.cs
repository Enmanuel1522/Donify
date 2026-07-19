using Donify.API.Data;
using Donify.API.Models.Entities;
using Donify.API.Repositories.Interfaces;

namespace Donify.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IDonorRepository Donors { get; set; }
        public IDonationRepository Donations { get; set; }
        public ICampaignRepository Campaigns { get; set; }
        public ICategoryRepository Categories { get; set; }
        public IProjectRepository Projects { get; set; }
        public IStaffRepository Staffs { get; set; }
        public IReceiptRepository Receipts { get; set; }
        public IUserRepository Users { get; set; }

        public UnitOfWork(  
            DataContext context, 

            IDonorRepository Donors,
            IDonationRepository Donations,
            ICampaignRepository Campaigns,
            ICategoryRepository Categories,
            IProjectRepository Projects,
            IStaffRepository Staffs,
            IReceiptRepository Receipts,
            IUserRepository Users
            )

        {
            _context = context;
            this.Donors = Donors;
            this.Donations = Donations;
            this.Campaigns = Campaigns;
            this.Categories = Categories;
            this.Projects = Projects;
            this.Staffs = Staffs;
            this.Receipts = Receipts;
            this.Users = Users;
        }

        public async Task<int> SaveAsync() =>
            await _context.SaveChangesAsync();

    }
}
