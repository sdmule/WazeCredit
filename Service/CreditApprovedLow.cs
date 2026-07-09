using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class CreditApprovedLow : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            //Have a different logic to calculate approval limit
            //we will hardcode to 50% of Salary
            return creditApplication.Salary * .5;
        }
    }
}
