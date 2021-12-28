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

    public SelectQueryInputModel()
    {
    }

    public SelectQueryInputModel(string tableName, List<string> selectColumnNames, AsOfSystemModel asOfSystemModel, List<DbWhereModel> whereModels, List<DbSortModel> sort, int limit, int offset)
    {
        TableName = tableName;
        SelectColumnNames = selectColumnNames;
        AsOfSystemModel = asOfSystemModel;
        WhereModels = whereModels;
        Sort = sort;
        Limit = limit;
        Offset = offset;
    }
}
    
public class DbWhereModel {
    
    [JsonProperty("whereFields")]
    public List<DbWhereFieldModel> WhereFields;
    
    [JsonProperty("operator")]
    public OperatorType Operator;

    public DbWhereModel()
    {
    }

    public DbWhereModel(List<DbWhereFieldModel> whereFields, OperatorType @operator)
    {
        WhereFields = whereFields;
        Operator = @operator;
    }
}
    
public class DbWhereFieldModel {
    
    [JsonProperty("fieldName")]
    public string FieldName;
    
    [JsonProperty("fieldValue")]
    public object FieldValue;
    
    [JsonProperty("operator")]
    public OperatorType Operator;

    public DbWhereFieldModel()
    {
    }

    public DbWhereFieldModel(string fieldName, object fieldValue, OperatorType @operator)
    {
        FieldName = fieldName;
        FieldValue = fieldValue;
        Operator = @operator;
    }
}
    
public class DbSortModel {
    
    [JsonProperty("fieldName")]
    public string FieldName;
    
    [JsonProperty("sortType")]
    public SortType SortType;

    public DbSortModel()
    {
    }

    public DbSortModel(string fieldName, SortType sortType)
    {
        FieldName = fieldName;
        SortType = sortType;
    }
}
    
public class KeyValueModel {
    
    [JsonProperty("key")]
    public string Key;
    
    [JsonProperty("value")]
    public object Value;

    public KeyValueModel()
    {
    }

    public KeyValueModel(string key, object value)
    {
        Key = key;
        Value = value;
    }
}
    
public class DbPageableModel {
    
    [JsonProperty("pageNumber")]
    public int PageNumber;
    
    [JsonProperty("pageSize")]
    public object PageSize;

    public DbPageableModel()
    {
    }

    public DbPageableModel(int pageNumber, object pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
    
public class SelectQueryOutputModel {
    
    [JsonProperty("pageNumber")]
    public int PageNumber;
    
    [JsonProperty("totalPageSize")]
    public int TotalPageSize;
    
    [JsonProperty("rows")]
    public List<object> Rows;

    public SelectQueryOutputModel()
    {
    }

    public SelectQueryOutputModel(int pageNumber, int totalPageSize, List<object> rows)
    {
        PageNumber = pageNumber;
        TotalPageSize = totalPageSize;
        Rows = rows;
    }
}
    
public class AsOfSystemModel {
    
    [JsonProperty("type")]
    public AsOfSystemType Type;
    
    [JsonProperty("value")]
    public string Value;

    public AsOfSystemModel()
    {
    }

    public AsOfSystemModel(AsOfSystemType type, string value)
    {
        Type = type;
        Value = value;
    }
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
