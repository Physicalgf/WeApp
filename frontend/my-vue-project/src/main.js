import Vue from 'vue'
import App from './App.vue'
import router from './router'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import axios from 'axios'

const instance = axios.create({
  baseURL: 'https://localhost:44311'
})

Vue.prototype.$http = instance
Vue.use(ElementUI)
Vue.config.productionTip = false


new Vue({
  router,
  render: function (h) { return h(App) }
}).$mount('#app')
