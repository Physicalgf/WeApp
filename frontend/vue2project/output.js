module.exports = {
    chainWebpack: config => {
      config.module
        .rule('compile')
        .test(/\.js$/)
        .exclude
          .add(/node_modules/)
          .end()
        .use('babel')
          .loader('babel-loader')
          .options({
            presets: ['@babel/preset-env']
          });
    }
  };
  