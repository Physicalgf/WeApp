<template>
    <el-container>
        <el-header><H2>注册</H2></el-header>
        <el-main>
            <h3 style="display: inline-block;">账号 ：</h3> <el-input v-model="FormData.username" placeholder="请输入账号"></el-input>
            <br>
            <h3 style="display: inline-block;">密码 ：</h3><el-input v-model="FormData.password" placeholder="请输入密码" type="password"></el-input>
        </el-main>
        <el-footer>
            <el-button type="primary" @click="register">注册</el-button>
        </el-footer>
    </el-container>
</template>

<script>
import axios from 'axios'
import Router from '../router'

export default {
  data () {
    return {
      FormData:
            {
              username: '',
              password: ''
            }
    }
  },
  methods: {
    register () {
      axios.post('https://localhost:44311/Login/CreateAcc', this.FormData)
        .then(response => {
          if (response.data.code === 200) {
            console.log('注册成功')
            Router.push('/')
          }
        })
    }
  }
}

</script>

<style>
.el-header{
    margin-top: 100px;
}
.el-input{
  width: 300px;
}
</style>
