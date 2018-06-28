﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bing.Domains.Entities
{
    /// <summary>
    /// 键列表比较器
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public class KeyListComparator<TKey>
    {
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="newList">新实体集合</param>
        /// <param name="oldList">旧实体集合</param>
        /// <returns></returns>
        public KeyListCompareResult<TKey> Compare(IEnumerable<TKey> newList, IEnumerable<TKey> oldList)
        {
            if (newList == null)
            {
                throw new ArgumentNullException(nameof(newList));
            }

            if (oldList == null)
            {
                throw new ArgumentNullException(nameof(oldList));
            }

            var newEntities = newList.ToList();
            var oldEntities = oldList.ToList();
            var createList = GetCreateList(newEntities, oldEntities);
            var updateList = GetUpdateList(newEntities, oldEntities);
            var deleteList = GetDeleteList(newEntities, oldEntities);

            return new KeyListCompareResult<TKey>(createList, updateList, deleteList);
        }

        /// <summary>
        /// 获取创建列表
        /// </summary>
        /// <param name="newList">新实体列表</param>
        /// <param name="oldList">旧实体列表</param>
        /// <returns></returns>
        private List<TKey> GetCreateList(List<TKey> newList, List<TKey> oldList)
        {
            return newList.Except(oldList).ToList();
        }

        /// <summary>
        /// 获取更新列表
        /// </summary>
        /// <param name="newList">新实体列表</param>
        /// <param name="oldList">旧实体列表</param>
        /// <returns></returns>
        private List<TKey> GetUpdateList(List<TKey> newList, List<TKey> oldList)
        {
            return newList.FindAll(id => oldList.Exists(t => t.Equals(id)));
        }

        /// <summary>
        /// 获取删除列表
        /// </summary>
        /// <param name="newList">新实体列表</param>
        /// <param name="oldList">旧实体列表</param>
        /// <returns></returns>
        private List<TKey> GetDeleteList(List<TKey> newList, List<TKey> oldList)
        {
            return oldList.Except(newList).ToList();
        }
    }
}