namespace AIDentify.Models.Enums
{
    public enum PaymentStatues
    {
        Pending,    // Waiting for admin approval
        Completed,  // Approved by admin, subscription activated
        Failed      // Rejected by admin or expired
    }
}
