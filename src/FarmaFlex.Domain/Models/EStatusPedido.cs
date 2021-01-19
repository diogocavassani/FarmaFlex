using System.ComponentModel;

namespace APIFarmaFlex.Domain.Models
{
    public enum StatusEnumPedido : byte
    {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Iniciado")]
        Iniciado = 2,
        [Description("Em Entrega")]
        EmEntrega = 3,
        [Description("Finalizado")]
        Finalizado = 4,
        [Description("Cancelado")]
        Cancelado = 5,
    }
}
