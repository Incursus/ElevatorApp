using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElevatorApp.ResultFilters;

public class FloorsResultFilterAttribute : ResultFilterAttribute
{
    /*
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null || resultFromAction.StatusCode is < 200 or >= 300)
        {
            await next();
            return;
        }


        resultFromAction.Value = this.GetResult(
            mapper,
            resultFromAction.Value,
            paginationDetails,
            context);

        await next();
    }
    */

    /*
    protected override object GetResult(IMapper mapper, object result, Paging paginationDetails, ResultExecutingContext context)
    {
        var flights = mapper.Map<IEnumerable<Instance>>(result, options => options.AfterMap(FilterContent(context)));

        return new Response<Instance> { Data = flights, Paging = paginationDetails };
    }
    */

    /*
    private static Action<object, IEnumerable<FlightBase>> FilterContent(ResultExecutingContext context)
    {
        return (_, mappedFlights) =>
        {
            foreach (var mappedFlight in mappedFlights.OfType<Instance>())
            {
                if (!ContentLadderQueryExtractor.IsStatusRequested(context))
                {
                    mappedFlight.StatusDetails = null;
                }

                if (!ContentLadderQueryExtractor.IsPredictedSeatsRequested(context) &&
                    !ContentLadderQueryExtractor.IsActualSeatsRequested(context))
                {
                    mappedFlight.SeatingCapacity = null;
                }
                else if (!ContentLadderQueryExtractor.IsPredictedSeatsRequested(context))
                {
                    mappedFlight.SeatingCapacity.Predicted = null;
                }
                else if (!ContentLadderQueryExtractor.IsActualSeatsRequested(context))
                {
                    mappedFlight.SeatingCapacity.Actual = null;
                }

                if (!ContentLadderQueryExtractor.IsExtendedSchedulesRequested(context))
                {
                    mappedFlight.BookingClasses = null;
                    mappedFlight.FreightClasses = null;
                    mappedFlight.Restrictions = null;
                    mappedFlight.PlaneChangeWithoutAircraftChange = null;
                    mappedFlight.OnTimePerformance = null;
                    mappedFlight.InFlightServices = null;
                    mappedFlight.AutomatedCheckIn = null;
                    mappedFlight.ElectronicTicketing = null;
                    mappedFlight.SecureFlightIndicator = null;
                    mappedFlight.GovernmentApproval = null;
                }
            }
        };
}
*/
}