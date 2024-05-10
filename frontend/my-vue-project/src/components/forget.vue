<template>
    <el-container>
        <div>
         <canvas ref="canvas" @click="createFirework" />
        </div>
        <el-header><H2>忘记密码</H2></el-header>
        <el-main>
            <h4 style="display: inline-block;margin-right: 20px;">请输入账号:</h4><el-input v-model="FormData.username" placeholder="请输入用户名"></el-input>
        </el-main>
        <el-footer>
            <el-button type="primary" size="medium" @click="GetPassword">点击查看密码</el-button>
        </el-footer>
    </el-container>
</template>

<script>
export default {
  data () {
    return {
      FormData:
            {
              username: ''
            }
    }
  },
  methods: {
    GetPassword () {
      if (!this.FormData.username) {
        this.$message.error('账号不能为空！')
        return
      }
      this.$http.post('/Login/ForgetPass', this.FormData)
        .then(response => {
          if (response.data.code === '200') {
            this.$message.success(response.data.data)
          } else {
            this.$message.error(response.data.messge)
          }
        })
    }
  }
}

</script>

<style scoped>
.el-header{
  margin-top: 200px;
}
.el-input{
    width: 300px;
}
</style>
