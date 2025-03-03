namespace backend.Dto
{
    public class createOrganization
    {
        public Guid Id { get; set; }
        public string BankName { get; set; }
        public int BranchCode { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }
    }
}
