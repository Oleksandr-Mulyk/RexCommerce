﻿namespace RexCommerce.CatalogGrpcService.Data
{
    public interface ICategory
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<IProduct> Products { get; set; }
    }
}
