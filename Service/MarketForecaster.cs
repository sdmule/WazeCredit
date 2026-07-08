using WazeCredit.Models;

namespace WazeCredit.Service
{
    public class MarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            //Call API to do some complex calculations and current stock market forecast
            //For course purpose we will hardcode the result

            return new MarketResult
            {
                MarketCondition = Models.MarketCondition.StableUp
            };
        }
    }
}
