<template>
    <el-container>
        <el-header><H2>注册</H2></el-header>
        <el-main>
            <h3 style="display: inline-block;">账号 ：</h3> <el-input v-model="FormData.username" style="width: 300px;" placeholder="请输入账号"></el-input>
            <br>
            <h3 style="display: inline-block;">密码 ：</h3><el-input v-model="FormData.password" style="width: 300px;" placeholder="请输入密码" type="password"></el-input>
        </el-main>
        <el-footer>
            <el-button type="primary" @click="register">注册</el-button>
        </el-footer>
    </el-container>
</template>

<script>
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
      if (!this.FormData.username || !this.FormData.password) {
        this.$message.error('账号或密码不能为空！')
        return
      }
      this.$http.post('/Login/CreateAcc', this.FormData)
        .then(response => {
          console.log(response.data.code)
          if (response.data.code === '200') {
            this.$message({
              message: response.data.messge,
              type: 'success'
            })
            Router.push('/login')
          } else {
            this.$message.error(response.data.messge)
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
