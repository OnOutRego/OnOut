using OnOut.Application.Features.Event;
namespace OnOut.Application.Features.Rego;
public class RegoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public EventDto Event { get; set; }
    public bool IsPaid { get; set; }
    public bool IsTransferable { get; set; }
    public bool IsRefundable { get; set; }

}
