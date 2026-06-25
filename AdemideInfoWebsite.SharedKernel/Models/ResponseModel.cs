using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.SharedKernel.Models;

//production grade response model for api responses with pagination support without exposing the internal implementation details of the api response, has data,status,statusCode,message and pagination properties.
// using record type to make it immutable and thread-safe, and use generics to make it reusable for different types of data.
public record ResponseModel<T>(T Data, bool Status, int StatusCode, string Message, PaginationModel? Pagination = null);
//public record PaginationModel(int PageNumber, int PageSize, int TotalRecords, int TotalPages);

public class PaginationModel
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    //public PaginationModel(
    //    int pageNumber,
    //    int pageSize,
    //    int totalRecords,
    //    int totalPages)
    //{
    //    PageNumber = pageNumber;
    //    PageSize = pageSize;
    //    TotalRecords = totalRecords;
    //    TotalPages = totalPages;
    //}
}
