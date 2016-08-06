using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum RegFrom { 
        wechat,
        app,
        wap
    }

    public enum Status {
        normal,
        pending,
        freeze
    }

    public enum PaymentType {
        none,
        alipay,
        tenpay,
        bank,
        paypal,
        card,
        offline,
        wxpay
    }

    public enum OrderType {
        NONE,
        SELL,
        RESERVE,
        RECHARGE,
        MARKET
    }

    public enum OrderStatus {
        NONE,
        WAIT_PAYMENT,
        PAYMENT_SUCCESS,
        PAYMENT_ERROR,
        SELLER_CANCEL,
        USER_CANCEL,
        WAIT_SURE,
        SHIPPED,
        RECEIVED,
        SURE,
        SYSTEM_STOP,
        FINISH,
        CHECKED
    }

    public enum FeedbackStatus { 
        NONE,
        PROCESSING,
        OVER,
        WAIT_AGREE
    }

}
