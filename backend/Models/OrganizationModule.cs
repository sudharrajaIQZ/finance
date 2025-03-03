using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class OrganizationModule
    {
        [Key]
        public Guid Id { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int BranchCode { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }

    }
}
