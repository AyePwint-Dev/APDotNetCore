namespace DotNetTrainingBatch4.PizzaApi.Query
{
    public class PizzaQuery
    {
        //const => don't know where it was used
        //static {get} => to know references No. and not editable
        public static string PizzaOrderQuery { get; } =
            @"Select po.*,p.Pizza,p.Price From [dbo].[Tbl_PizzaOrder] po
            inner join Tbl_Pizza p on p.PizzaId = po.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";

        //Need to search by (ctr + ,)
        //public const string PizzaOrderDetailQuery = @"...Enter Query here...";
        public static string PizzaOrderDetailQuery { get; } = 
            @"Select pod.*,pe.PizzaExtraName,pe.Price From [dbo].[Tbl_PizzaOrderDetail] pod
            inner join Tbl_PizzaExtra pe on pe.PizzaExtraId = pod.PizzaExtraId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
    }
}
