mergeInto(LibraryManager.library, {
  Hello: function () {
    console.log("Hello!");
  },

  ShowAdvReward: function () {
    AdController.show().then((result) => {
      console.log("your code -- to reward user");
      console.log("Ad result:", result.message);
    }).catch((result ) => {
      console.log("no reward -- video skipped");
      console.log("Ad error:", result.message);
    });
  }
});