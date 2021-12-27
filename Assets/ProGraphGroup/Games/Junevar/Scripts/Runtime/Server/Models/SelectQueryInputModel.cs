using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ProGraphGroup.Games.Junevar.Server.Models
{
    
public class SelectQueryInputModel {
    
    [JsonProperty("tableName")]
    public string TableName;
    
    [JsonProperty("selectColumnNames")]
    public List<string> SelectColumnNames;
    
    [JsonProperty("asOfSystemModel")]
    public AsOfSystemModel AsOfSystemModel;
    
    [JsonProperty("whereModels")]
    public List<DbWhereModel> WhereModels;
    
    [JsonProperty("sort")]
    public List<DbSortModel> Sort;
    
    [JsonProperty("limit")]
    public int Limit;
    
    [JsonProperty("offset")]
    public int Offset;
    
}
    
public class DbWhereModel {
    
    [JsonProperty("whereFields")]
    public List<DbWhereFieldModel> WhereFields;
    
    [JsonProperty("operator")]
    public OperatorType Operator;
    
}
    
public class DbWhereFieldModel {
    
    [JsonProperty("fieldName")]
    public string FieldName;
    
    [JsonProperty("fieldValue")]
    public object FieldValue;
    
    [JsonProperty("operator")]
    public OperatorType Operator;
    
}
    
public class DbSortModel {
    
    [JsonProperty("fieldName")]
    public string FieldName;
    
    [JsonProperty("sortType")]
    public SortType SortType;
    
}
    
public class KeyValueModel {
    
    [JsonProperty("key")]
    public string Key;
    
    [JsonProperty("value")]
    public object Value;
    
}
    
public class DbPageableModel {
    
    [JsonProperty("pageNumber")]
    public int PageNumber;
    
    [JsonProperty("pageSize")]
    public object PageSize;
    
}
    
public class SelectQueryOutputModel {
    
    [JsonProperty("pageNumber")]
    public int PageNumber;
    
    [JsonProperty("totalPageSize")]
    public int TotalPageSize;
    
    [JsonProperty("rows")]
    public List<object> Rows;
    
}
    
public class AsOfSystemModel {
    
    [JsonProperty("type")]
    public AsOfSystemType Type;
    
    [JsonProperty("value")]
    public string Value;
    
}
    
public enum OperatorType {
    None = 0 ,
    GreaterThan = 1 ,
    GreaterThanOrEqual = 2 ,
    LessThan = 3 ,
    LessThanOrEqual = 4 ,
    Equal = 5 ,
    NotEqual = 6 ,
    And = 7 ,
    Or = 8 ,
    Not = 9 ,
    Like = 10 ,
    NotLike = 11 
}
    
public enum SortType {
    None = 0,
    ASC = 1,
    DESC = 2
}
public enum AsOfSystemType {
    None = 0,
    Time = 1
}

}
