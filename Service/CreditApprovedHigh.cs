using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditApprovedHigh : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            //Have a different logic to calculate approval limit
            //we will hardcode to 30% of Salary
            return creditApplication.Salary * .3;
        }
    }
}
