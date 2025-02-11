﻿using Microsoft.AppCenter.Crashes;
using OSBookReviewMAUI.Helpers;
using OSBookReviewMAUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSBookReviewMAUI.Services
{
    public class AuthorDataClass : IDataStore<Author>
    {
        public IApiHelper ApiHelper => DependencyService.Get<ApiHelper>();
        public AuthorDataClass()
        {

        }

        public async Task<IEnumerable<Author>> GetListAsync()
        {
            return await GetAuthors();
        }
        // returns an author by id
        public async Task<IEnumerable<Author>> GetListAsync(int AID)
        {
            return await GetAuthorByID(AID);
        }

        // returns  a list of authors by name
        public async Task<IEnumerable<Author>> GetListAsync(string authorname)
        {
            return await GetAuthorsByName(authorname);
        }


        // get the list of auhtors with the criteria of a name serach
        // in the event of error return an empty list
        private async Task<List<Author>> GetAuthorsByName(string authorname)
        {
            try
            {
                string weblink = $"api/book/GetAuthorByName?name={authorname}";

                var response = await ApiHelper.GetList<Author>(weblink);

                return response;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);

                List<Author> res = new();

                return res;
            }
        }
        // get the list of Top 50 Authors in the event of error return an empty list
        private async Task<List<Author>> GetAuthors()
        {
            try
            {
                string weblink = "/api/book/getauthors";
                var response = await ApiHelper.GetList<Author>(weblink);

                return response;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                List<Author> res = new();

                return res;
            }
        }
        //gets authors by id
        private async Task<IEnumerable<Author>> GetAuthorByID(int id)
        {
            try
            {
                string weblink = $"api/book/GetAuthorByID?aid={id}";

                var response = await ApiHelper.GetList<Author>(weblink);

                return response;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);

                List<Author> res = new();

                return res;
            }
        }
    }
}
