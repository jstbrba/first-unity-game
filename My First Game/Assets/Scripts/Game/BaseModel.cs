using UnityEngine;

public abstract class BaseModel : ScriptableObject, IModel
{
    public IContext Context { get {  return _context; } }
    private IContext _context;
    public virtual void Initialise(IContext context)
    {
        _context = context;
    }
}
