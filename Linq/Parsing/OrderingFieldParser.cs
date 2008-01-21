using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rubicon.Data.Linq.Clauses;
using Rubicon.Data.Linq.DataObjectModel;
using Rubicon.Utilities;

namespace Rubicon.Data.Linq.Parsing
{
  public class OrderingFieldParser
  {
    private readonly OrderingClause _orderingClause;
    private readonly IDatabaseInfo _databaseInfo;

    public OrderingFieldParser (OrderingClause orderingClause, IDatabaseInfo databaseInfo)
    {
      ArgumentUtility.CheckNotNull ("orderingClause", orderingClause);
      ArgumentUtility.CheckNotNull ("databaseInfo", databaseInfo);

      _orderingClause = orderingClause;
      _databaseInfo = databaseInfo;

    }

    public OrderingField GetField ()
    {
      return ParseMemberExpression ((MemberExpression) _orderingClause.Expression.Body);
    }

    private OrderingField ParseMemberExpression (MemberExpression expression)
    {
      FromClauseBase fromClause = ClauseFinder.FindFromClauseForExpression (_orderingClause, expression.Expression);
      //ParameterExpression tableIdentifier = expression.Expression as ParameterExpression;
      //if (tableIdentifier == null)
      //  throw ParserUtility.CreateParserException ("table identifier", expression.Expression, "member access in order by ", _orderingClause.Expression);
      Table table = DatabaseInfoUtility.GetTableForFromClause (_databaseInfo, fromClause);
      Column column = DatabaseInfoUtility.GetColumn (_databaseInfo, table, expression.Member);
      OrderingField orderingField = new OrderingField(column,_orderingClause.OrderDirection);
      return orderingField;
    }


    //private ICriterion ParseMemberExpression (MemberExpression expression)
    //{
    //  ParameterExpression tableIdentifier = expression.Expression as ParameterExpression;
    //  if (tableIdentifier == null)
    //    throw ParserUtility.CreateParserException ("table identifier", expression.Expression, "member access in where condition", _whereClause.BoolExpression);

    //  FromClauseBase fromClause = ClauseFinder.FindFromClauseForExpression (_whereClause, tableIdentifier);
    //  Table table = DatabaseInfoUtility.GetTableForFromClause (_databaseInfo, fromClause);
    //  Column column = DatabaseInfoUtility.GetColumn (_databaseInfo, table, expression.Member);
    //  return column;
    //}
  }
}