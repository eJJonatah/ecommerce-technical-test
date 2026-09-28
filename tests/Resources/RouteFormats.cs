namespace TEcomerc.Tests.Resources;

static class RouteFormats
{
    public const string AUTH_LOGIN = "/auth/login";
    public const string API_ORDERS = "/api/orders";
    public const string FMT_API_ORDERS__PAGE__PAGESIZE__INCLUDEITEMS = "/api/orders?page={0}&pagesize={1}&includeitems={2}";
    public const string FMT_API_ORDERS__ID = "/api/orders/{0}";
    public const string FMT_API_ORDERS__ID__CANCEL = "/api/orders/{0}/cancel";
}