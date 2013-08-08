﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;

namespace HTMLFetchAndParse
{
    public class HTMLParser
    {
        public string ParseHtmlSingleNodeAtrrib(string p, string xpath, string attribute, bool isString = false)
        {
            HtmlDocument doc = new HtmlDocument();
            if (isString == false)
                doc.Load(p);
            else
                doc.LoadHtml(ConvertToHtml(p));

            //Console.WriteLine(doc.DocumentNode.InnerHtml);
            var tsNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tsNode != null)
            {
                return tsNode.Attributes[attribute].Value;
            }

            return null;
        }

        public ICollection<String> ParseHtmlColloctionNodeAttribute(String filename, String xpath, String attribute, String checkXpath, String checkValue)
        {
            List<String> lists = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(filename);
            // Console.WriteLine(doc.DocumentNode.InnerHtml);

            var tsNodeCollection = doc.DocumentNode.SelectNodes(xpath);
            if (tsNodeCollection != null)
            {
                foreach (var tsNode in tsNodeCollection)
                {
                    var CheckNodes = tsNode.SelectNodes(checkXpath);
                    if (CheckNodes != null)
                    {
                        foreach (var CheckNode in CheckNodes)
                        {
                            if (CheckNode != null && CheckNode.InnerText.Contains(checkValue))
                            {
                                lists.Add(CheckNode.Attributes[attribute].Value);
                            }
                        }
                    }
                }
            }

            return lists;
        }


        public ICollection<String> ParseHtmlColloctionNodeAttribute(string content, string xpath, string attribute, bool isString = false, bool isHtml = false)
        {
            List<String> lists = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            if (isString == false)
                if (isHtml == false)
                    doc.Load(content);
                else
                    doc.LoadHtml(content);
            else
                doc.LoadHtml(ConvertToHtml(content));
            // Console.WriteLine(doc.DocumentNode.InnerHtml);

            var tsNodeCollection = doc.DocumentNode.SelectNodes(xpath);
            if (tsNodeCollection != null)
            {
                foreach (var tsNode in tsNodeCollection)
                {
                    lists.Add(tsNode.Attributes[attribute].Value);
                }
            }

            return lists;
        }

        public ICollection<String> ParseHtmlColloctionNodeContent(string filename, string xpath, bool innnerHtml = false, bool isString = false, bool isHtml = false)
        {
            List<String> lists = new List<string>();
            HtmlDocument doc = new HtmlDocument();
            if (isString == false)
                if (isHtml == false)
                    doc.Load(filename);
                else
                    doc.LoadHtml(filename);
            else
                doc.LoadHtml(ConvertToHtml(filename));
            // Console.WriteLine(doc.DocumentNode.InnerHtml);

            var tsNodeCollection = doc.DocumentNode.SelectNodes(xpath);
            if (tsNodeCollection != null)
            {
                foreach (var tsNode in tsNodeCollection)
                {
                    if (innnerHtml == true)
                    {
                        lists.Add(tsNode.OuterHtml);
                    }
                    else
                        lists.Add(tsNode.InnerText);
                }
            }

            return lists;
        }

        public string ParsePattern(string file, string pattern)
        {

            throw new NotImplementedException();
        }

        private string ConvertToHtml(string str)
        {
            return string.Format("<html><head></head><body>{0}</body></html>", str);
        }

        public string ParseRegexSingleNode(string content, string retex)
        {
            throw new NotImplementedException();
        }

        public string ParseHtmlSingleNodeContent(string p, string xpath, bool isString = false, bool isHtml = false)
        {
            HtmlDocument doc = new HtmlDocument();
            if (isString == false)
                if (isHtml == false)
                    doc.Load(p);
                else
                    doc.LoadHtml(p);
            else
                doc.LoadHtml(ConvertToHtml(p));

            //Console.WriteLine(doc.DocumentNode.InnerHtml);
            var tsNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tsNode != null)
            {
                return tsNode.InnerText;
            }

            return null;
        }
    }
    }
