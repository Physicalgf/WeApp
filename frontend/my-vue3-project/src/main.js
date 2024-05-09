import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'; //初始化路由
import ElementPlus from 'element-plus'  //初始化element-plus
import 'element-plus/dist/index.css' //初始化element-plus
import App from './App.vue' //初始化App

//导入登陆页面
import LoginPage from '@/components/Login/LoginHome.vue'; 
//导入注册页面
import RegisPage from '@/components/Login/RegisterPage.vue';
//导入忘记密码
import ForgetPage from '@/components/Login/ForgetPass.vue';
//导入首页页面
import HomePage from '@/components/Home/HomePage.vue';     


const router = createRouter({
    history: createWebHistory(),
    routes: [
        { path: '/', component: LoginPage }, //默认跳转路由
        {path: '/regispage', component: RegisPage}, //跳转注册页面
        {path: '/forgetpage', component: ForgetPage},//跳转忘记密码页面
        { path: '/home', component: HomePage }  //跳转首页
     
    ]
  });

try {
    // 创建应用实例
    const app = createApp(App)

    // 使用ElementPlus，考虑到按需加载，这里保持不变
    app.use(ElementPlus)

     //调用前置地址    
      //app.provide('apiBaseUrl', 'http://121.40.185.118:44311') //服务器地址     
     app.provide('apiBaseUrl', 'https://localhost:44311')//本地地址
     //挂载路由
     app.use(router);

    // 挂载到指定的DOM元素上
    app.mount('#app')
} catch (error) {
    console.error("应用启动出错:", error);
    // 可以考虑添加错误处理逻辑，比如回退到错误页面、上报错误信息等
}
