variable "build_id" {}

terraform {
  backend "s3" {
    bucket  = "cv-overkill-tf-state"
    key     = "aws-infrastructure-experience"
    region  = "eu-west-1"
  }
}

variable "region" {
  default = "eu-west-1"
}

provider "aws" {
  region = "eu-west-1"
}

module "experience" {
  source = "github.com/mtranter/cv-overkill-terraform?ref=v1.0//modules/tf-cv-overkill-aurelia-module"
  website_files = ["app-bundle.js"]
  relative_source_path = "/../src/ui/dist/"
  region = "${var.region}"
  module_name = "experience"
}

data "template_file" "task_definition" {
    template = "${file("${path.module}/experience-service.json")}"

    vars {
      image_tag = "${var.build_id}"
    }
}

module "experience_backend" {
  source                        = "github.com/mtranter/cv-overkill-terraform?ref=v1.5//modules/ecs-service"
  alb_listener_rule_priority    = 85
  alb_condition_field           = "path-pattern"
  alb_condition_values          = "/experience*"
  service_name                  = "experience-service"
  alb_container_name            = "experience-service"
  service_port                  = "80"
  task_definition               = "${data.template_file.task_definition.rendered}"
  desired_count                 = 1
  health_check_path = "/experience"

}
