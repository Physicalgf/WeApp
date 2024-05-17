<template>
    <div class="manage-musics">
        <div class="manage-musicsearch" style="margin-top: 20px;margin-left: 30px;">
            <el-input style="width: 300px;" v-model="KeyWord" placeholder="请输入内容" clearable> <el-button
                    @click="MusicSearch" slot="append" icon="el-icon-search">搜索</el-button></el-input>
            <el-button type="primary" style="margin-left: 20px;" @click="dialogVisibleAdd = true">新增</el-button>
        </div>



        <div class="manage-musiclist" style="margin-top: 30px;margin-left: 100px;">
            <el-table :data="tableData" style="width: 90%;margin-left: 30px;">
                <el-table-column prop="id" label="主键" v-if="showIdColumn"> </el-table-column>
                <el-table-column prop="music_Name" label="歌名" width="600px"> </el-table-column>
                <el-table-column prop="music_Person" label="歌手"></el-table-column>
                <el-table-column prop="create_Time" label="创建时间"></el-table-column>
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-button size="mini" @click="editMusic(scope.row)">编辑</el-button>
                        <el-button size="mini" type="danger" @click="deleteMusic(scope.row)">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>


        <div>
            <el-dialog :visible.sync="dialogVisibleAdd" title="新增音乐" width="50%">
                <el-form>
                    <!-- 在这里添加表单字段，例如： -->
                    <el-form-item label="歌曲名称">
                        <el-input v-model="form.music_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="歌手">
                        <el-input v-model="form.music_Person"></el-input>
                    </el-form-item>
                    <el-form-item label="歌词">
                        <el-input type="textarea" v-model="form.music_Lyric"></el-input>
                    </el-form-item>

                    <el-form-item label="上传歌曲封面">
                        <el-upload class="upload-demo" action="http://121.40.185.118:44311/Musics/UploadPictures"
                            :on-preview="handlePreview" :on-remove="handleRemove" :before-remove="beforeRemove"
                            multiple :limit="3" :on-exceed="handleExceed" :file-list="fileList">
                            <el-button size="small" type="primary">点击上传</el-button>
                            <div slot="tip" class="el-upload__tip">只能上传jepg文件</div>
                        </el-upload>
                    </el-form-item>

                    <el-form-item label="上传歌曲">
                        <el-upload class="upload-demo" action="http://121.40.185.118:44311/Musics/UploadMusic"
                            :on-preview="handlePreview" :on-remove="handleRemove" :before-remove="beforeRemove"
                            multiple :limit="3" :on-exceed="handleExceed" :file-list="fileList">
                            <el-button size="small" type="primary">点击上传</el-button>
                            <div slot="tip" class="el-upload__tip">只能上传mp3文件</div>
                        </el-upload>
                    </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisibleAdd = false">取 消</el-button>
                    <el-button type="primary" @click="submitAdd">确 定</el-button>
                </div>
            </el-dialog>
        </div>


        <div>
            
            <!-- 编辑对话框 -->
            <el-dialog :visible.sync="dialogVisibleEdite" title="编辑音乐" width="50%">
                <el-form :model="selectedRow">
                    <el-form-item label="歌曲名称">
                        <el-input v-model="selectedRow.music_Name"></el-input>
                    </el-form-item>
                    <el-form-item label="歌手">
                        <el-input v-model="selectedRow.music_Person"></el-input>
                    </el-form-item>
                    <el-form-item label="歌词">
                        <el-input type="textarea" v-model="selectedRow.music_Lyric"></el-input>
                    </el-form-item>
                </el-form>
                <div slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisibleEdite = false">取 消</el-button>
                    <el-button type="primary" @click="updateForm">确 定</el-button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data(){
        return{
            fileList: [],
            selectedFile: null,
            showIdColumn: false, // 初始隐藏id列
            dialogVisibleAdd: false, // 对话框是否可见
            dialogVisibleEdite:false,
            KeyWord:'',
            tableData: [],
            selectedRow: {
                id: '',
                music_Name: '',
                music_Person: '',
                music_Lyric: ''
            },
            form:{
                id:'',
                music_Name:'',
                music_Person:'',
                music_Lyric:'',
                file: null,
            }
        }
    },
    created(){
        try {
            this.$http.get(`/Musics/GetMusicList?KeyWord=${this.KeyWord}`).then(res => {
                if (res.data.code === '200') {
                    this.tableData = res.data.data
                } else {
                    this.$message.error(res.data.msg)
                }
            })
        } catch (error) {
            this.$message.error('请求失败，请检查网络连接')
        }
    },
    methods:{
        MusicSearch(){
            try {
                this.$http.get(`/Musics/GetMusicList?KeyWord=${this.KeyWord}`).then(res => {
                    if (res.data.code === '200') {
                        this.tableData = res.data.data
                    } else {
                        this.$message.error(res.data.msg)
                    }
                })
            } catch(error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        submitAdd(){
            try{
                this.$http.post('/Musics/AddMusic',this.form).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisibleAdd=false
                        this.$message.success('新增成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })
            } catch(error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        editMusic(row) {
            this.selectedRow = row;
            this.dialogVisibleEdite = true;
        },
        updateForm(){
            try{
                this.$http.post('/Musics/EditeMusic',this.selectedRow).then(res => {
                    if (res.data.code === '200') {
                        this.dialogVisibleEdite=false
                        this.$message.success('修改成功')
                        this.MusicSearch()
                    } else {
                        this.$message.error(res.data.messge)
                    }
                })

            } catch(error) {
                this.$message.error('请求失败，请检查网络连接')
            }
        },
        deleteMusic(row) {
            this.$confirm('确定要删除吗?', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                // 在这里添加删除请求的逻辑
               this.$http.get(`/Musics/DeleteMusic?Id=${row.id}`).then(res => {
                   if (res.data.code === '200') {
                       this.$message.success('删除成功')
                       this.MusicSearch()
                   }
               })
            }).catch(() => {
                // 用户取消删除操作
                console.log('删除操作已取消');
            });
        },
        handleFileChange(event) {
      // 保存用户选择的文件
      this.selectedFile = event.target.files[0];
    },
    uploadFile() {
      if (this.selectedFile) {
        // 创建一个FormData对象，用于发送文件数据
        let formData = new FormData();
        formData.append('file', this.selectedFile);
        
        this.$http.post('/Musics/UploadMusic', formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        }).then(response => {
          // 文件上传成功的处理逻辑
          if(response.data.code === '200'){
            this.$message.success('上传成功')
          }
        }).catch(error => {
          // 文件上传失败的处理逻辑
          this.$message.error('上传失败')
        });
        
      } else {
        console.error('No file selected');
      }
    },

    handleRemove(file, fileList) {
        console.log(file, fileList);
      },
      handlePreview(file) {
        console.log(file);
      },
      handleExceed(files, fileList) {
        this.$message.warning(`当前限制选择 3 个文件，本次选择了 ${files.length} 个文件，共选择了 ${files.length + fileList.length} 个文件`);
      },
      beforeRemove(file, fileList) {
        return this.$confirm(`确定移除 ${ file.name }？`);
      }
    }
}
</script>

<style scoped>
.upload-demo {
  /* 添加边框样式 */
  border: 1px solid #DCDFE6; /* 颜色可以根据需要调整 */
  border-radius: 4px; /* 可选，为边框添加圆角 */
  padding: 10px; /* 可选，增加内部间距 */
}
</style>