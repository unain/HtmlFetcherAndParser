﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace HTMLFetchAndParse {
    public class HTMLParser {
        public string SingleNodeAttribute(string content, string xpath,
            string attribute, bool isHTML = true, bool isFilePath = false ) {
            var doc = new HtmlDocument();
            if (isFilePath)
                doc.Load(content);
            else
                doc.LoadHtml(isHTML ? content : AddHTMLHeader(content));

            var tsNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tsNode != null) {
                return tsNode.Attributes[attribute].Value;
            }
            return null;
        }

        public ICollection<String> ColloctionNodeAttribute(string content,
            string xpath, string attribute, bool isHTML = true, bool isFilePath = false) {

            var lists = new List<string>();
            var doc = new HtmlDocument();
            if (isFilePath)
                doc.Load(content);
            else
                doc.LoadHtml(isHTML ? content : AddHTMLHeader(content));

            var tsNodeCollection = doc.DocumentNode.SelectNodes(xpath);
            if (tsNodeCollection != null) {
                foreach (var tsNode in tsNodeCollection) {
                    lists.Add(tsNode.Attributes[attribute].Value);
                }
            }
            return lists;
        }

        public ICollection<String> ColloctionNodeContent(
            string content, string xpath, bool extractHtml = false,
            bool isHTML = true, bool isFilePath = false) {

            var lists = new List<string>();
            var doc = new HtmlDocument();
            if (isFilePath)
                doc.Load(content);
            else
                doc.LoadHtml(isHTML ? content : AddHTMLHeader(content));

            var tsNodeCollection = doc.DocumentNode.SelectNodes(xpath);
            if (tsNodeCollection != null) {
                foreach (var tsNode in tsNodeCollection) {
                    if (extractHtml) {
                        lists.Add(tsNode.OuterHtml);
                    } else
                        lists.Add(tsNode.InnerText);
                }
            }
            return lists;
        }

        public string ParsePattern(string content, string pattern, bool isFilePath = true) {

            if (isFilePath)
                content = File.ReadAllText(content);

            Match matche = Regex.Match(content.Trim(), pattern, RegexOptions.IgnoreCase);
            if (matche.Success) {
                return matche.Groups[1].Value;
            }
            return null;
        }

        private string AddHTMLHeader(string str) {
            return string.Format("<html><head></head><body>{0}</body></html>", str);
        }

        public string ParseRegexSingleNode(string content, string retex) {
            throw new NotImplementedException();
        }

        public string SingleNodeContent(
            string content, string xpath, bool isHTML = true, bool isFilePath = false) {
            var doc = new HtmlDocument();
            if (isFilePath)
                doc.Load(content);
            else
                doc.LoadHtml(isHTML ? content : AddHTMLHeader(content));

            var tsNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tsNode != null) {
                return tsNode.InnerText;
            }
            return null;
        }
    }
}
