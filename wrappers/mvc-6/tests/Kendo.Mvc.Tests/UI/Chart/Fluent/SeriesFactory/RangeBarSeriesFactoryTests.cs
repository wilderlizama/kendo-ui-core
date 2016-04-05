﻿using Xunit;
using Kendo.Mvc.UI.Fluent;
using System;

namespace Kendo.Mvc.UI.Tests
{
    public class RangeBarSeriesFactoryTests
    {
        private readonly Chart<SalesData> chart;
        private readonly ChartSeriesFactory<SalesData> factory;

        public RangeBarSeriesFactoryTests()
        {
            chart = ChartTestHelper.CreateChart<SalesData>();
            factory = new ChartSeriesFactory<SalesData>(chart.Series);
        }

        [Fact]
        public void RangeBar_series_with_custom_data_should_set_Type()
        {
            factory.RangeBar(new int[] { });

            chart.Series[0].Type.ShouldEqual("rangeBar");
        }

        [Fact]
        public void RangeBar_series_with_custom_data_should_set_series_data()
        {
            var data = new int[] { 1, 2, 3 };

            factory.RangeBar(data);

            chart.Series[0].Data.ShouldBeSameAs(data);
        }

        [Fact]
        public void RangeBar_series_with_custom_data_should_return_builder()
        {
            var builder = factory.RangeBar(new int[] { });

            builder.ShouldBeType(typeof(ChartSeriesBuilder<SalesData>));
        }

        [Fact]
        public void RangeBar_series_with_custom_name_should_override_default_Name()
        {
            CreateSeries().Name("customName");

            chart.Series[0].Name.ShouldEqual("customName");
        }

        [Fact]
        public void RangeBar_series_with_members_should_set_Name()
        {
            factory.RangeBar("TotalSales", "RepSalesHigh");

            chart.Series[0].Name.ShouldEqual("Total Sales, Rep Sales High");
        }

        [Fact]
        public void RangeBar_series_with_members_and_category_should_not_set_Name()
        {
            factory.RangeBar("TotalSales", "RepSalesHigh", "RepName");

            chart.Series[0].Name.ShouldEqual("Total Sales, Rep Sales High");
        }

        [Fact]
        public void RangeBar_series_with_expression_should_set_FromField()
        {
            CreateSeries();

            chart.Series[0].FromField.ShouldEqual("TotalSales");
        }

        [Fact]
        public void RangeBar_series_with_expression_should_set_ToField()
        {
            CreateSeries();

            chart.Series[0].ToField.ShouldEqual("RepSalesHigh");
        }
        
        [Fact]
        public void RangeBar_series_with_expression_should_set_Type()
        {
            CreateSeries();

            chart.Series[0].Type.ShouldEqual("rangeBar");
        }
        

        [Fact]
        public void RangeBar_series_with_expression_should_not_set_Name()
        {
            CreateSeries();

            chart.Series[0].Name.ShouldEqual(null);
        }

        [Fact]
        public void RangeBar_series_with_expression_should_return_builder()
        {
            var builder = CreateSeries();

            builder.ShouldBeType(typeof(ChartSeriesBuilder<SalesData>));
        }

        [Fact]
        public void RangeBar_series_with_category_expression_should_set_FromField()
        {
            CreateSeriesWithCategory();

            chart.Series[0].FromField.ShouldEqual("TotalSales");
        }

        [Fact]
        public void RangeBar_series_with_category_expression_should_set_ToField()
        {
            CreateSeriesWithCategory();

            chart.Series[0].ToField.ShouldEqual("RepSalesHigh");
        }

        [Fact]
        public void RangeBar_series_with_category_expression_should_set_CategoryField()
        {
            CreateSeriesWithCategory();

            chart.Series[0].CategoryField.ShouldEqual("RepName");
        }

        [Fact]
        public void RangeBar_series_with_category_expression_should_set_Type()
        {
            CreateSeriesWithCategory();

            chart.Series[0].Type.ShouldEqual("rangeBar");
        }

        [Fact]
        public void RangeBar_series_with_category_expression_should_not_set_Name()
        {
            CreateSeriesWithCategory();

            chart.Series[0].Name.ShouldEqual(null);
        }
        
        [Fact]
        public void RangeBar_series_with_category_expression_should_return_builder()
        {
            var builder = CreateSeriesWithCategory();

            builder.ShouldBeType(typeof(ChartSeriesBuilder<SalesData>));
        }

        [Fact]
        public void RangeBar_series_created_with_named_expressions_should_set_FromField()
        {
            factory.RangeBar(fromExpression: s => s.TotalSales, toExpression: s => s.RepSalesHigh);

            chart.Series[0].FromField.ShouldEqual("TotalSales");
        }

        [Fact]
        public void RangeBar_series_created_with_named_expressions_should_set_ToField()
        {
            factory.RangeBar(fromExpression: s => s.TotalSales, toExpression: s => s.RepSalesHigh);

            chart.Series[0].ToField.ShouldEqual("RepSalesHigh");
        }

        [Fact]
        public void RangeBar_series_created_with_named_expressions_should_set_CategoryField()
        {
            factory.RangeBar(fromExpression: s => s.TotalSales, toExpression: s => s.RepSalesHigh, categoryExpression: s => s.RepName);

            chart.Series[0].CategoryField.ShouldEqual("RepName");
        }

        private ChartSeriesBuilder<SalesData> CreateSeries()
        {
            return factory.RangeBar(s => s.TotalSales, s => s.RepSalesHigh);
        }

        private ChartSeriesBuilder<SalesData> CreateSeriesWithCategory()
        {
            return factory.RangeBar(s => s.TotalSales, s => s.RepSalesHigh, s => s.RepName);
        }
    }
}