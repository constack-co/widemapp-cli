const path = require("path");
module.exports = {
  transpileDependencies: [
    'vuetify'
  ],
  configureWebpack: {
    resolve: {
      alias: {
        '@Guest': path.resolve(__dirname, 'src/components/guest'),
        '@Auth': path.resolve(__dirname, 'src/components/auth'),
        '@Services': path.resolve(__dirname, 'src/services'),
        '@Common': path.resolve(__dirname, 'src/common'),
        '@Mixin': path.resolve(__dirname, 'src/common/mixins'),
      }
    }
  }
}