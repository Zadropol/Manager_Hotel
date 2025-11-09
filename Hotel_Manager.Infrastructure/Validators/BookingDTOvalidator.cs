using FluentValidation;
using Hotel_Manager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Validators
{
    public class BookingDTOvalidator : AbstractValidator<BookingDTO>
    {
        public BookingDTOvalidator()
        {
            RuleFor(x => x.GuestId)
                .GreaterThan(0)
                .WithMessage("El ID del huésped debe ser mayor a 0.");

            RuleFor(x => x.RoomId)
                .GreaterThan(0)
                .WithMessage("El ID de la habitación debe ser mayor a 0.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("La fecha de Check-In es obligatoria.")
                .Must(BeValidDate).WithMessage("Formato de fecha inválido (dd-MM-yyyy).");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("La fecha de Check-Out es obligatoria.")
                .Must(BeValidDate).WithMessage("Formato de fecha inválido (dd-MM-yyyy).");

            RuleFor(x => new { x.CheckInDate, x.CheckOutDate })
                .Must(d => CompareDates(d.CheckInDate, d.CheckOutDate))
                .WithMessage("La fecha de salida debe ser posterior a la de entrada.");
        }

        private bool BeValidDate(string date)
        {
            return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);
        }

        private bool CompareDates(string checkIn, string checkOut)
        {
            if (!BeValidDate(checkIn) || !BeValidDate(checkOut)) return true;
            var inDate = DateTime.ParseExact(checkIn, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var outDate = DateTime.ParseExact(checkOut, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return outDate > inDate;
        }
    }
}
