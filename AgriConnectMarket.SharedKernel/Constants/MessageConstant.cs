namespace AgriConnectMarket.SharedKernel.Constants
{
    public static class MessageConstant
    {
        public const string COMMON_RETRIVE_SUCCESS_MESSAGE = "Retrieving data successfully!";
        public const string COMMON_CREATE_SUCCESS_MESSAGE = "Create successfully!";
        public const string COMMON_UPDATE_SUCCESS_MESSAGE = "Update successfully!";
        public const string COMMON_DELETE_SUCCESS_MESSAGE = "Delete successfully!";
        public const string UNKNOWN_ERROR = "An unexpected error occured!";
        public const string EXISTING_USERNAME = "Username already exist";
        public const string LOGIN_SUCCESS = "Login success!";
        public const string EMAIL_NOT_FOUND = "This email is not registered yet";
        public const string PROFILE_NOT_FOUND = "Can not find the requested profile";
        public const string ACCOUNT_NOT_FOUND = "Can not find the requested account";
        public const string DEACTIVE_ACCOUNT_SUCCESS = "Deactive account successfully!";
        public const string PROFILE_ID_NOT_FOUND = "Can not find any profile with this ID";
        public const string WRONG_CREDENTIALS = "Wrong username or password. Please try again!";
        public const string NOT_AUTHENTICATED_USER = "User not authenticated!";
        public const string ACCOUNT_NOT_VERIFIED = "Please verify your email before continuing!";
        public const string ACCOUNT_VERIFIED = "Account verified!";
        public const string FARM_NOT_FOUND = "Can not found farm(s)";
        public const string BAN_FARM_SUCCESS = "Banned!";
        public const string SEASON_NOT_FOUND = "Can not found season(s)";
        public const string CLOSE_SEASON_SUCCESS = "Close season successfully";
        public const string ADDRESS_NOT_FOUND = "Can not found address(es)";
        public const string EXISTING_CATEGORY = "This category already exist!";
        public const string CATEGORY_NOT_FOUND = "Can not found category(s)";
        public const string PRODUCT_NOT_FOUND = "Can not found product(s)";
        public const string PRODUCT_CONFLICT = "This product already exist, please choose another one!";
        public const string FARM_MISSING_INFO_FOR_SELL = "This farm is missing requirement informations for selling. " +
            "Please ask the farmer provide them or they will not be able to sell their products";
        public const string FARM_SELL_ALLOWING_SUCCESS = "This farm is allowed for selling products successfully!";
        public const string FARM_MARKED_MALL = "This farm is marked as a mall farm successfully!";
        public const string BATCH_CODE_CONFLICT = "This batch code already exist, please choose another one!";
        public const string BATCH_NOT_FOUND = "Can not found batch(es)";
        public const string FARM_MISSING_PREFIX = "Please configure the prefix before creating a batch";
        public const string FAVORITE_FARM_NOT_FOUND = "Can not found favorite farm(s)";
        public const string CART_NOT_INIT = "This cart was not initialized yet. Please try again!";
        public const string CART_ITEM_NOT_FOUND = "Can not found this item";
        public const string ORDER_NOT_FOUND = "Can not found order(s)";
        public const string EVENT_TYPE_NOT_FOUND = "Can not found event type(s)";
        public const string CARE_EVENT_NOTE_FOUND = "Can not found event(s)";
        public const string INVALID_CHAIN = "This chain is not verified";
        public const string TRANSACTION_FAIL = "The transaction was failed";
    }
}
