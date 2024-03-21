<template>  
    <navbar />
    <EventPeopleList :title="title" :isAthlete="isAthlete"/>
</template>
  
<script setup lang="ts">

import EventPeopleList from "../components/EventPeopleList.vue"
    import { defineProps } from "vue";
  import navbar from "../components/Navbar.vue"
  
  defineProps<{ title: String, isAthlete: Boolean }>();
  </script>