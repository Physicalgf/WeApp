<template>
    <div class="manage-user">
        <div class="manage-usersearch" style="margin-top: 20px;margin-left: 30px;">
            <el-input style="width: 300px;" v-model="KeyWord"   placeholder="请输入内容"  clearable> <el-button @click="Usersearch" slot="append" icon="el-icon-search">搜索</el-button></el-input>
        </div>
        <div class="manage-userlist" style="margin-top: 30px;margin-left: 100px;">
            <el-table :data="tableData" style="width: 90%;margin-left: 30px;">
                 <el-table-column prop="user_Name" label="姓名" width="800px"> </el-table-column>
                 <el-table-column prop="create_Time" label="创建时间"></el-table-column>
            </el-table>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
        KeyWord: '',
        tableData: []
    }
  },
   created() {
    try {
        this.$http.get('/Login/GetUserList',{KeyWord:this.KeyWord}).then(res => {
        if (res.data.code === '200') {
            this.tableData=res.data.data
        } else {
          this.$message.error(res.data.messge)
        }
      })
    } catch (error) {
      console.error("请求数据时发生错误:", error);
      // 可以在此处处理错误，比如展示错误消息给用户
    }
  },
  methods: {
    Usersearch() {
        try {
            this.$http.get(`/Login/GetUserList?KeyWord=${this.KeyWord}`).then(res => {
                if (res.data.code === '200') {
                    this.tableData=res.data.data
                }
            })
        } catch (error) {
            this.$message.error("请求数据时发生错误:" + error);
        }
    }
    }
}
</script>