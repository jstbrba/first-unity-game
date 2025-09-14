public class TimeController : BaseController<TimeModel, TimeView>
{
    public TimeController(TimeModel model, TimeView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);
    }
}