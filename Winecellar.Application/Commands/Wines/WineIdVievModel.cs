namespace Winecellar.Application.Commands.Wines
{
    public class WineIdViewModel
    {
        public Guid Id { get; set; }

        public WineIdViewModel(Guid id) => Id = id;
    }
}