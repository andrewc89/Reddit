namespace Reddit.Things.API.Meta
{
    using Extensions;
    using System;

    public class SubredditMetaData
    {
        #region Constructor

        public SubredditMetaData()
        {
        }

        #endregion

        #region Properties

        public int AccountsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime CreatedUTC { get; set; }

        public string Description { get; set; }

        public string DescriptionHtml { get; set; }

        public string DisplayName { get; set; }

        public SubredditHeader Header { get; set; }

        public string ID { get; set; }

        public Thing Thing { get; set; }

        public bool Over18 { get; set; }

        public string PublicDescription { get; set; }

        public long Subscribers { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        #endregion

        #region Factory

        internal static SubredditMetaData Create(SimpleJSON.JObject Json)
        {
            var Temp = new SubredditMetaData();

            Temp.AccountsActive = Json["accounts_active"].IntValue;
            Temp.Created = Json["created"].DoubleValue.ToDateTime();
            Temp.CreatedUTC = Json["created_utc"].DoubleValue.ToDateTime();
            Temp.Description = Json["description"].StringValue;
            Temp.DescriptionHtml = Json["description_html"].StringValue;
            Temp.DisplayName = Json["display_name"].StringValue;
            if (Json["header_size"].Count > 0)
            {
                Temp.Header = new SubredditHeader();
                Temp.Header.ImageUrl = Json["header_img"].StringValue;
                Temp.Header.ImageWidth = Json["header_size"][0].IntValue;
                Temp.Header.ImageHeight = Json["header_size"][1].IntValue;
                Temp.Header.Title = Json["header_title"].StringValue;
            }
            Temp.ID = Json["id"].StringValue;
            Temp.Thing = Thing.Get(Json["name"].StringValue);
            Temp.Over18 = Json["over18"].BooleanValue;
            Temp.PublicDescription = Json["public_description"].StringValue;
            Temp.Subscribers = Json["subscribers"].LongValue;
            Temp.Title = Json["title"].StringValue;
            Temp.Url = Json["url"].StringValue;

            return Temp;
        }

        #endregion
    }
}