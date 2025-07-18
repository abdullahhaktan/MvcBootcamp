﻿using BusinnessLayer.Abstract;
using BusinnessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {

        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public void CategoryUpdate(Category category)
        {
           _categoryDal.Update(category);
        }

        public Category GetByID(int id)
        {
            return _categoryDal.Get(c => c.CategoryID == id);
        }

        public List<Category> GetList()
        {
            return _categoryDal.List();
        }
    }
}