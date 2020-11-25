﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H.Core;
using HtmlAgilityPack;

namespace H.Searchers
{
    /// <summary>
    /// 
    /// </summary>
    public class YandexSearcher : Module, ISearcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<string>> Search(string query) => await Task.Run(() =>
        {
            var url = $"https://www.yandex.ru/search/?text={query}";

            var web = new HtmlWeb();
            var document = web.Load(new Uri(url));

            return document.DocumentNode
                .SelectNodes("//a[@href]")
                .Where(i => i.Attributes.Contains("tabindex") && i.Attributes["tabindex"].Value == "2")
                .Select(i => i.Attributes["href"].Value)
                .ToList();
        }).ConfigureAwait(false);
    }
}
