﻿using System.Reflection;
using Bing.Helpers;
using Bing.Ui.Angular.Enums;
using Bing.Ui.Configs;
using Bing.Ui.Extensions;
using Bing.Utils.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Str = Bing.Helpers.Str;

namespace Bing.Ui.Angular.Tables.Resolvers
{
    /// <summary>
    /// 表格列表达式解析器
    /// </summary>
    public class ColumnExpressionResolver
    {
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly ModelExpression _expression;

        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfig _config;

        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        /// 初始化一个<see cref="ColumnExpressionResolver"/>类型的实例
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="config">配置</param>
        private ColumnExpressionResolver(ModelExpression expression, IConfig config)
        {
            if (expression == null || config == null)
            {
                return;
            }
            _expression = expression;
            _config = config;
            _memberInfo = expression.GetMemberInfo();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="config">配置</param>
        public static void Init(ModelExpression expression, IConfig config)
        {
            new ColumnExpressionResolver(expression, config).Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _config.SetAttribute(UiConst.Column, Str.FirstLower(_expression.Name));
            _config.SetAttribute(UiConst.Title, Reflection.GetDisplayNameOrDescription(_memberInfo));
            InitType();
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        private void InitType()
        {
            if (Reflection.IsBool(_memberInfo))
            {
                _config.SetAttribute(UiConst.Type, TableColumnType.Bool);
            }
            else if (Reflection.IsDate(_memberInfo))
            {
                _config.SetAttribute(UiConst.Type, TableColumnType.Date);
            }
        }
    }
}
