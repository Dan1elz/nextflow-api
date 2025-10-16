namespace nextflow.Domain.Enums;

public enum OrderStatus : byte
{
    PendingPayment = 1,   // Aguardando pagamento
    PaymentConfirmed = 2, // Pagamento confirmado
    Canceled = 3,         // Pedido cancelado
    Refunded = 4          // Pedido reembolsado
}
