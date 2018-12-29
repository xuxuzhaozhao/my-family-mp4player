<template>
  <v-app id="inspire" dark>
    <v-navigation-drawer v-model="drawer" fixed clipped app>
      <v-radio-group v-model="selected" column>
        <v-treeview
          :open="open"
          :items="items"
          hoverable
          transition
          open-on-click
          activatable
          active-class="grey lighten-4 indigo--text"
        >
          <template slot="prepend" slot-scope="{ item, open, leaf }">
            <v-radio v-if="!item.isDirctory" :value="item"/>
            <v-icon v-if="item.isDirctory">{{ open ? 'mdi-folder-open' : 'mdi-folder' }}</v-icon>
            <v-icon v-else>{{ fileIcon }}</v-icon>
          </template>
        </v-treeview>
      </v-radio-group>
    </v-navigation-drawer>
    <v-toolbar color="red" dense fixed clipped-left app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title class="mr-3 align-center">
        <span class="title">XXZZ - MP4</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-layout row align-center style="max-width: 650px">
        <v-text-field
          placeholder="Search..."
          single-line
          append-icon="search"
          color="white"
          hide-details
        ></v-text-field>
      </v-layout>
    </v-toolbar>
    <v-content>
      <v-container fill-height>
        <v-layout justify-center align-center>
          <v-flex shrink>
            <div>
              <video ref="video" controls="controls" style="width: 100%">
                <!-- <source v-if="modifiedSouce" :src=""> -->
              </video>
            </div>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import { Component, Vue, Prop, Emit, Watch } from "vue-property-decorator";
import HelloWorld from "@/components/HelloWorld.vue"; // @ is an alias to /src
import axios from "axios";

@Component({
  components: {
    HelloWorld
  }
})
export default class Home extends Vue {
  private drawer: boolean = true;
  private open: Array<string> = ["public"];
  private selected: any = {};

  private fileIcon: string = "mdi-movie";

  private items: Array<any> = [
    {
      name: "正在加载中...",
      fullName: ""
    }
  ];

  created() {
    this.getMenus();
  }

  @Watch("selected")
  onSelectedItemChanged(val: any, old: any) {
    let fullname = val.fullName.split("\\wwwroot").pop();

    $(this.$refs.video).attr("src", `http://${location.host}${fullname}`);
    // if (confirm(`是否播放：${val.name}`)) {

    // }
  }

  getFileExtension(filename: string) {
    return filename.slice(((filename.lastIndexOf(".") - 1) >>> 0) + 2);
  }

  getMenus() {
    return new Promise((resolve, reject) => {
      axios
        .get(`/api/file/getmenus`)
        .then(res => {
          this.items = res.data;
          resolve(true);
        })
        .catch(e => reject(e));
    });
  }
}
</script>

<style>
.v-radio {
  margin: 0 !important;
}
</style>
