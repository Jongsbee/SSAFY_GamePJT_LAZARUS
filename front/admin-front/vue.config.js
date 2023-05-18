const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
    publicPath: "./",
    transpileDependencies: true,
    devServer: {
        port: 3001, // 원하는 포트 번호로 변경
        historyApiFallback: true,
    },
});
