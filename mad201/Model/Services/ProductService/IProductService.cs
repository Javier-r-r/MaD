using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ProductDao;
using Model.Daos.PropertyDao;
using Model.Services.ProductService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.ProductService
{
    public interface IProductService
    {
        [Inject]
        IProductDao ProductDao { set; }
        [Inject]
        IPropertyDao PropertyDao { set; }

        [Transactional]
        ProductDto FindByProductName(String productName);
        [Transactional]
        List<ProductDto> FindByProductCategory(long categoryId, int pageNumber, int pageSize);
        [Transactional]
        List<ProductDto> SearchFilters(long restaurantId, int pageNumber, int pageSize, long categoryId, string name);
        [Transactional]
        List<ProductProperty> FindPropertiesByProductId(long productId, int pageNumber, int pageSize);
        [Transactional]
        List<Property> ViewAllProperties();
    }
}
