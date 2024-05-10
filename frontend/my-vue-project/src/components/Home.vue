<template>
    <el-container>
        <el-header>
            <div><h2>最好的我们，最好的你</h2></div>
        </el-header>
        <el-main>
            <el-carousel indicator-position="outside" loop>
            <el-carousel-item style="height: 1600px;" v-for="(image, index) in images" :key="index">
                <img :src="require(`@/image/image${index + 1}.jpg`)"  :alt="`Image ${index + 1}`" style="width: 100%; height: 1000px;">
            </el-carousel-item>
            </el-carousel>
        </el-main>
        <el-footer></el-footer>
    </el-container>
</template>


<script>
export default {
  data() {
    return {
      images: Array.from({ length: 4 }).map((_, i) => `image${i + 1}.jpg`),
    };
  },
};
</script>

<style scoped>
.el-header{
    margin-top: 10px;
}
.el-main{
    height: 1600px;
}
.el-carousel{
    margin-left: 400px;
    height: 1600px;
    width: 70%;
}
</style>