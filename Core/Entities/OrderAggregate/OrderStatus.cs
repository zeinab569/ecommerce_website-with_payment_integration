using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "paymentReceived")]
        PaymentReceived,
        [EnumMember(Value = "paymentFailed")]
        PaymentFailed

    }
}
