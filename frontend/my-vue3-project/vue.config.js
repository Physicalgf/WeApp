const { defineConfig } = require('@vue/cli-service')
const { ElementPlusResolver } = require('unplugin-vue-components/resolvers')

module.exports = defineConfig({
  pluginOptions: {
    'style-resources-loader': {
      preProcessor: 'less',
      patterns: [
        // 引入Element Plus全局样式
        './node_modules/element-plus/packages/theme-chalk/src/index.less'
      ]
    },
    'unplugin-vue-components': {
      resolvers: [ElementPlusResolver()],
      dts: true, // 生成类型声明文件（可选）
    },
    'unplugin-auto-import': {
      // 自动导入Element Plus的API（如`ElMessageBox.confirm`）
      imports: ['element-plus'],
      dts: true, // 生成类型声明文件（可选）
    },
  }
})
