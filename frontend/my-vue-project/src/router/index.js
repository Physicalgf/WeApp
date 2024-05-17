import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '@/components/Login'
import Home from '@/components/Home'
import Register from '@/components/register'
import Forget from '@/components/forget'
import MusicsHome from '@/components/Musics/MusicHome'
import ManageHome from '@/components/Manage/ManageHome'
import MusicPlayer from '@/components/Musics/MusicPlayer'
import MusicGc from '@/components/Musics/MusicGc'

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

  
  //音乐首页
  {
    path: '/MusicsHome',
    name: 'MusicsHome',
    component: MusicsHome
  },
  //音乐播放器
  {
    path: '/MusicPlayer',
    name: 'MusicPlayer',
    component: MusicPlayer
  },
  //音乐歌词 MusicGc
  {
    path: '/MusicGc',
    name: 'MusicGc',
    component: MusicGc
  }
  
  
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
