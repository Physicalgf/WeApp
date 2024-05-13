import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/components/Login'
import Home from '@/components/Home'
import Register from '@/components/register'
import Forget from '@/components/forget'
import MusicsHome from '@/components/Musics/MusicHome'
import ManageHome from '@/components/Manage/ManageHome'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/home',
    name: 'Home',
    component: Home
  },
  {
    path: '/forget',
    name: 'Forget',
    component: Forget
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },

  //后台管理
  {
    path: '/ManageHome',
    name: 'ManageHome',
    component: ManageHome
  },

  
  //音乐
  {
    path: '/MusicsHome',
    name: 'MusicsHome',
    component: MusicsHome
  }

  
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
